using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ScenarioBasedVideoUploader.Compiler.CommandHandlers;

namespace ScenarioBasedVideoUploader
{

    public delegate void EnabledStatusHandler( bool isEnabled );
    public delegate void StringHandler( string str );
    public delegate void IndexStringHandler( int index, string str );
    public delegate void IndexHandler( int index );
    public delegate void FillKeyValueDataGrid( List<Parameter> records, int selectedRowIndex );
    public delegate void FillStringsHandler( List<string> lines );

    public class DebuggerEngine
    {
        public Settings                                                             Settings                        { get; private set; }
        public bool                                                                 AutorunNextScenario             { get; set; }
        
        private readonly IReadOnlyList<ICommandHandler>                             _availableCommandHandlers;
        private readonly Compiler.Compiler                                          _compiler;

        private string                                                              _currentQueuedScenarioName;
        private readonly List< IsExecutedWrapper<string> >                          _queuedScenarioNames;
        private readonly Dictionary<string, List< IsExecutedWrapper<ICommand> > >   _queuedScenario2Commands;

        private List<Parameter>                                                     _inputParameters;
        private Dictionary<string, Dictionary<string, string> >                     _scenarioName2OutputParameters;
        
        private DebuggerWebDriver                                                   _debuggerWebDriver;
        
#region Events

        public event EnabledStatusHandler           OnQueuedScenariosSectionEnabledStatusChanged; 
        public event EnabledStatusHandler           OnParametersSectionEnabledStatusChanged; 
        public event EnabledStatusHandler           OnCurrentScenarioSectionEnabledStatusChanged; 

        public event StringHandler                  OnCreateNewQueuedScenario;
        public event IndexHandler                   OnSetQueuedScenarioIndex;
        
        public event FillKeyValueDataGrid           OnSetInputParametersDataGrid;
        public event FillKeyValueDataGrid           OnSetOutputParametersDataGrid;
        
        public event FillStringsHandler             OnSetCurrentScenarioLines;
        public event StringHandler                  OnAddCurrentScenarioLine;
        public event IndexHandler                   OnDeleteCurrentScenarioLine;
        public event IndexStringHandler             OnInsertCurrentScenarioLine;
        public event IndexStringHandler             OnUpdateCurrentScenarioLine;
        
        public event IndexHandler                   OnCurrentScenarioLineExecuted;
        public event IndexHandler                   OnSetCurrentScenarioLineIndex;
        
        public event FillStringsHandler             OnSetQueuedScenarios;
        public event IndexHandler                   OnSelectQueuedScenarioLineIndex;
        
        public event EnabledStatusHandler           OnSetAutorunNextScenario;
        
        public event StringHandler                  OnSetConfigFileName;
#endregion        
        
        public DebuggerEngine()
        {
            _compiler                           = new Compiler.Compiler();
            
            _availableCommandHandlers           = _compiler.CommandHandlers;
            
            Settings                            = InitializeSettings();
        
            _queuedScenarioNames                = new List< IsExecutedWrapper< string > >();
            _queuedScenario2Commands            = new Dictionary< string, List< IsExecutedWrapper< ICommand > > >();
            
            _inputParameters                    = new List< Parameter >();
            _scenarioName2OutputParameters      = new Dictionary< string, Dictionary< string, string > >();
            
            _currentQueuedScenarioName          = string.Empty;
            
            _debuggerWebDriver                  = new DebuggerWebDriver();
        }

        private void Reset()
        {
            _currentQueuedScenarioName = string.Empty;
            
            _queuedScenarioNames.Clear();
            _queuedScenario2Commands.Clear();
            _inputParameters.Clear();
            _scenarioName2OutputParameters.Clear();
            _debuggerWebDriver.Clear();
            
            OnSetInputParametersDataGrid( _inputParameters, _inputParameters.Count > 0 ? 0 : -1 );
            SetAutorunNextScenario( false );
            OnSetCurrentScenarioLines( new List< string >() );
            OnSetQueuedScenarios( _queuedScenarioNames.Select( e => e.Item ).ToList() );
            OnSetOutputParametersDataGrid( new List< Parameter >(), -1 );
            
            InitializeUI();
        }
        
        private void SetAutorunNextScenario( bool isEnabled )
        {
            AutorunNextScenario = isEnabled;
            OnSetAutorunNextScenario( isEnabled );
        }
        
        public void CreateNewConfig()
        {
            Reset();
            
            var fileNames = GetFileNames( Settings.FolderForScenarios, Settings.ScenarioFileExtension );

            LoadScenarios( fileNames );
            
            OnSetConfigFileName( string.Empty );
        }

        private void LoadScenarios( string[] fileNames )
        {
            var parameters = new HashSet< string >();
            var currentFileParameters = new HashSet< string >();
            foreach( var fileName in fileNames )
            {
                var lines = File.ReadLines( fileName ).ToArray();

                try
                {
                    currentFileParameters.Clear();

                    var commands = _compiler.GetCompiledCommands( lines );

                    foreach( var command in commands )
                        currentFileParameters.UnionWith( command.GetParameterNames() );

                    //добавлять только если сценарий валиден
                    parameters.UnionWith( currentFileParameters );

                    var scenarioName = GetScenarioName( fileName, Settings.FolderForScenarios, Settings.ScenarioFileExtension );
                    _queuedScenario2Commands[ scenarioName ] =
                        commands.Select( e => new IsExecutedWrapper< ICommand >( e ) ).ToList();
                    _queuedScenarioNames.Add( new IsExecutedWrapper< string >( scenarioName ) );
                }
                catch( Exception e )
                {
                    MessageBox.Show( $"{fileName} - {e.Message}", "Error in the scenario file" );
                }
            }

            foreach( var parameter in parameters )
                _inputParameters.Add( new Parameter( parameter, string.Empty ) );

            OnQueuedScenariosSectionEnabledStatusChanged( true );
            OnParametersSectionEnabledStatusChanged( true );
            OnSetInputParametersDataGrid( _inputParameters, _inputParameters.Count > 0 ? 0 : -1 );

            SetAutorunNextScenario( true );

            OnSetQueuedScenarios( _queuedScenarioNames.Select( e => e.Item ).ToList() );
            if( _queuedScenarioNames.Count > 0 )
            {
                OnSelectQueuedScenarioLineIndex( 0 );
                OnCurrentScenarioSectionEnabledStatusChanged( true );
            }
        }


        public void InitializeUI()
        {
            OnQueuedScenariosSectionEnabledStatusChanged( false );
            OnParametersSectionEnabledStatusChanged( false );
            OnCurrentScenarioSectionEnabledStatusChanged( false );
        }

        private Settings InitializeSettings()
        {
            if( File.Exists( Settings._FILE_NAME ) )
                return Serializer.DeserializeFromFile<Settings>( Settings._FILE_NAME );

            return Settings.CreateDefaultSettingsFile();
        }
        
        public string GetSettingsText()
        {
            return Serializer.Serialize( Settings );
        }

        public static IEnumerable< string > GetScenarioNames( IReadOnlyList< string > fileNames, string folderName, string extension )
        {
            return fileNames.Select( e => GetScenarioName( e, folderName, extension ) );
        }

        public static string GetScenarioName( string fullFileName, string folderName, string extension )
        {
            var fileName = fullFileName.Remove( 0, folderName.Length + 1 );

            var extensionLength = string.IsNullOrEmpty( extension ) ? 0 : extension.Length + 1;
            
            return fileName.Remove( fileName.Length - extensionLength );
        }
        
        public static string[] GetFileNames( string folderName, string fileExtension )
        {
            if( !Directory.Exists( folderName ) )
                Directory.CreateDirectory( folderName );

            return Directory.EnumerateFiles( folderName, $"*.{fileExtension}" ).ToArray();
        }

        public void CreateNewScenario( string newScenarioName )
        {
            if( string.IsNullOrEmpty( newScenarioName ) )
                throw new ArgumentException( nameof(newScenarioName) );

            _currentQueuedScenarioName = newScenarioName;
            
            _queuedScenarioNames.Add( new IsExecutedWrapper< string >( newScenarioName ));
            _queuedScenario2Commands[ newScenarioName ] = new List< IsExecutedWrapper< ICommand > >();
            
            OnCreateNewQueuedScenario( newScenarioName );
            OnSetQueuedScenarioIndex( _queuedScenarioNames.Count - 1 );
            
            if( _queuedScenarioNames.Count > 0 )
                OnCurrentScenarioSectionEnabledStatusChanged( true );
            
            OnSetCurrentScenarioLines( new List< string >() );   
            
            SetAutorunNextScenario( false );
        }
        
        public string[] GetListOfAvailableCommands()
        {
            return _availableCommandHandlers.Select( e => e.CommandName ).ToArray();
        }

        public string GetCommandTemplate( string commandName )
        {
            var selectedHandler = _availableCommandHandlers.FirstOrDefault( 
                handler => string.Compare( commandName, handler.CommandName, StringComparison.InvariantCultureIgnoreCase ) == 0 );
            
            if( selectedHandler is null )
                return string.Empty;
             
            return selectedHandler.GetCommandTemplate();
        }

        public void AddCommandToCurrentScenario( string text )
        {
            if( string.IsNullOrEmpty( text.Trim() ) )
                return;
            
            var commandList = _queuedScenario2Commands[ _currentQueuedScenarioName ];
            var initialCount = commandList.Count;

            IReadOnlyList< ICommand > commands;
            try
            {
                commands = _compiler.GetCompiledCommands( new[]{ text });
                commandList.AddRange( commands.Select( e => new IsExecutedWrapper< ICommand >( e ) ) );
            }
            catch( Exception e )
            {
                MessageBox.Show( $"Incorrect command '{text}': " + e.Message, "Error" );
                return;
            }
            
            if( initialCount != commandList.Count )
                for( int i = initialCount; i < commandList.Count; i++ )
                    OnAddCurrentScenarioLine( commandList[ i ].Item.ToString() );

            var inputParameterNames = new HashSet<string>( commands.SelectMany( e => e.GetParameterNames() ) );

            AddNewInputParameters( inputParameterNames );
        }

        public void DeleteCurrentScenarioLine( int selectedIndex )
        {
            if( _queuedScenario2Commands[ _currentQueuedScenarioName ].Count <= selectedIndex )
                throw new Exception($"Incorrect index of the command line: {selectedIndex}");
            
            _queuedScenario2Commands[ _currentQueuedScenarioName ].RemoveAt( selectedIndex );
            
            OnDeleteCurrentScenarioLine( selectedIndex );
        }

        public void InsertCommandToCurrentScenario( string text, int selectedIndex )
        {
            if( string.IsNullOrEmpty( text.Trim() ) )
                return;
            
            var commandList = _queuedScenario2Commands[ _currentQueuedScenarioName ];
            var initialCount = commandList.Count;
            var addedCount = 0;
            
            ICommand[] commands;
            try
            {
                commands = _compiler.GetCompiledCommands( new[]{ text }).ToArray();
                
                commandList.InsertRange( selectedIndex, commands.Select( e => new IsExecutedWrapper< ICommand >( e ) ).ToArray() );
                
                addedCount = commands.Length;
            }
            catch( Exception e )
            {
                MessageBox.Show( $"Incorrect command '{text}': " + e.Message, "Error" );
                return;
            }
            
            if( initialCount != commandList.Count )
                for( int i = 0; i < addedCount; i++ )
                    OnInsertCurrentScenarioLine( selectedIndex + i, commandList[ selectedIndex + i ].Item.ToString() );
            
            var inputParameterNames = new HashSet<string>( commands.SelectMany( e => e.GetParameterNames() ) );

            AddNewInputParameters( inputParameterNames );
        }

        public void UpdateCommandInCurrentScenario( string text, int selectedIndex )
        {
            if( string.IsNullOrEmpty( text.Trim() ) )
                return;
            
            var commandList = _queuedScenario2Commands[ _currentQueuedScenarioName ];
            
            ICommand? command;
            try
            {
                command = _compiler.GetCompiledCommands( new[]{ text }).FirstOrDefault();
                
                if( command is null )
                    return;
                
                commandList[ selectedIndex ] = new IsExecutedWrapper< ICommand >( command );
            }
            catch( Exception e )
            {
                MessageBox.Show( $"Incorrect command '{text}': " + e.Message, "Error" );
                return;
            }
            
            OnUpdateCurrentScenarioLine( selectedIndex, commandList[ selectedIndex ].Item.ToString() );
            
            var inputParameterNames = new HashSet<string>( command.GetParameterNames() );

            AddNewInputParameters( inputParameterNames );
        }

        public void ProcessInputParameter( string name, string value, int selectedRowIndex )
        {
            if( selectedRowIndex == -1 && _inputParameters.Any( e => string.Compare( e.Name, name, StringComparison.InvariantCultureIgnoreCase ) == 0 ) )
            {
                MessageBox.Show($"Duplicated parameter name: {name}", "Error");
                return ;
            }
            
            if( selectedRowIndex == -1 )
            {
                _inputParameters.Add( new Parameter( name, value ) );
                OnSetInputParametersDataGrid( _inputParameters, -1 );
            }
            else
            {
                if( string.IsNullOrEmpty( name.Trim() ) )
                {
                    _inputParameters.RemoveAt( selectedRowIndex );
                    OnSetInputParametersDataGrid( _inputParameters, -1 );
                }
                else
                    _inputParameters[ selectedRowIndex ] = new Parameter( name, value );
            }
        }
        
        public RowStatus GetCurrentScenarioRowStatus( int index )
        {
            var commandWrapper = _queuedScenario2Commands[ _currentQueuedScenarioName ][ index ];
            return new RowStatus( commandWrapper.Item.ToString(), commandWrapper.IsExecuted );
        }

        public int GetCurrentScenarioLineCount()
        {
            if( string.IsNullOrEmpty( _currentQueuedScenarioName ) || !_queuedScenario2Commands.ContainsKey( _currentQueuedScenarioName ) )
                return 0;

            return _queuedScenario2Commands[ _currentQueuedScenarioName ].Count;
        }
        
        public void RunScenario( int selectedIndex )
        {
            var initialIndex    = selectedIndex >= 0 ? selectedIndex : 0;
            var inputParameters = _inputParameters.ToDictionary( e => e.Name, e => e.Value );

            do
            {
                if( !RunScenarioCommands( initialIndex, inputParameters ) )
                    return;
                
                if( !AutorunNextScenario )
                    return;

                var scenarioNameIndex = _queuedScenarioNames.FindIndex( e => e.Item == _currentQueuedScenarioName );
                var nextScenarioIndex = scenarioNameIndex + 1;
                if( _queuedScenarioNames.Count <= nextScenarioIndex )
                    return;

                _currentQueuedScenarioName = _queuedScenarioNames[ nextScenarioIndex ].Item;
                OnSetCurrentScenarioLineIndex( -1 );
                OnSetQueuedScenarioIndex( nextScenarioIndex ); 
            } 
            while( true );
            
        }

        private bool RunScenarioCommands( int initialIndex, Dictionary< string, string > inputParameters )
        {
            var currentScenarioCommands = _queuedScenario2Commands[ _currentQueuedScenarioName ];
            if( currentScenarioCommands.Count == 0 )
                return true;

            if( !_scenarioName2OutputParameters.ContainsKey( _currentQueuedScenarioName ) )
                _scenarioName2OutputParameters[ _currentQueuedScenarioName ] = new Dictionary< string, string >();

            for( int index = initialIndex; index < currentScenarioCommands.Count; index++ )
            {
                OnSetCurrentScenarioLineIndex( index );

                if( !_debuggerWebDriver.Execute( currentScenarioCommands[ index ].Item, Settings, _currentQueuedScenarioName,
                       inputParameters, _scenarioName2OutputParameters[ _currentQueuedScenarioName ] ) )
                    return false;

                if( _debuggerWebDriver.OutputParametersChanged )
                    OnSetOutputParametersDataGrid( GetOutputParameters(), -1 );

                OnCurrentScenarioLineExecuted( index );

                currentScenarioCommands[ index ].IsExecuted = true;

                var lineIndexToSelect = index + 1 < _queuedScenario2Commands[ _currentQueuedScenarioName ].Count ? index + 1 : -1;
                OnSetCurrentScenarioLineIndex( lineIndexToSelect );
            }

            return true;
        }

        public void ExecuteScenarioLine( int selectedIndex )
        {
            var currentScenarioCommands = _queuedScenario2Commands[ _currentQueuedScenarioName ];
            if( currentScenarioCommands.Count == 0 )
                return;
            
            if( selectedIndex == -1 )
                selectedIndex = 0;
            
            var inputParameters         = _inputParameters.ToDictionary( e => e.Name, e => e.Value );
            
            OnSetCurrentScenarioLineIndex( selectedIndex );
            
            if( !_scenarioName2OutputParameters.ContainsKey( _currentQueuedScenarioName ) )
                _scenarioName2OutputParameters[ _currentQueuedScenarioName ] = new Dictionary< string, string >();
            
            if( !_debuggerWebDriver.Execute( currentScenarioCommands[ selectedIndex ].Item, Settings, _currentQueuedScenarioName, inputParameters, _scenarioName2OutputParameters[ _currentQueuedScenarioName ] ) )
                return;
                            
            if( _debuggerWebDriver.OutputParametersChanged )
                OnSetOutputParametersDataGrid( GetOutputParameters(), -1 );
            
            OnCurrentScenarioLineExecuted( selectedIndex );
            
            currentScenarioCommands[ selectedIndex ].IsExecuted = true;
            
            var lineIndexToSelect = selectedIndex + 1 <  currentScenarioCommands.Count ? selectedIndex + 1 : -1;
            OnSetCurrentScenarioLineIndex( lineIndexToSelect );
        }

        private List< Parameter > GetOutputParameters()
        {
            var result = new List< Parameter >();

            foreach( var pair in _scenarioName2OutputParameters )
            {
                var scenarioName     = pair.Key;
                var outputParameters = pair.Value;

                foreach( var name2Value in outputParameters )
                    result.Add( new Parameter($"{scenarioName}-{name2Value.Key}", name2Value.Value ));                    
            }
            
            return result;
        }

        public void SetSettings( string text )
        {
            Settings = Serializer.Deserialize<Settings>( text );
        }
        
        public void ReloadSettings()
        {
            Settings = InitializeSettings();
        }

        public void SaveSettings()
        {
            Serializer.SerializeToFile( Settings, Settings._FILE_NAME );
        }

        public void SaveCurrentScenario()
        {
            if( string.IsNullOrEmpty( _currentQueuedScenarioName ) || _queuedScenario2Commands[ _currentQueuedScenarioName ].Count == 0 )
                return ;
            
            File.WriteAllLines( Path.Combine(Settings.FolderForScenarios, $"{_currentQueuedScenarioName}.{Settings.ScenarioFileExtension}"), _queuedScenario2Commands[ _currentQueuedScenarioName ].Select( e => e.Item.ToString() ) );
            
            MessageBox.Show("Current scenario has been saved");
        }

        public void SelectScenario( int selectedIndex )
        {
            if( selectedIndex == -1 )
            {
                _currentQueuedScenarioName = string.Empty;
                OnSetCurrentScenarioLines( new List< string >() );
                return;
            }
            
            var selectedScenarioName = _queuedScenarioNames[ selectedIndex ].Item;
            
            if( _currentQueuedScenarioName == selectedScenarioName )
                return;
            
            _currentQueuedScenarioName = selectedScenarioName;
            OnSetCurrentScenarioLines( _queuedScenario2Commands[ _currentQueuedScenarioName ].Select( e => e.Item.ToString() ).ToList() );
        }

        public void DeleteQueuedScenario( int selectedIndex )
        {
            if( selectedIndex == -1 )
                return ;
            
            var scenarioName = _queuedScenarioNames[ selectedIndex ].Item;
            
            _queuedScenarioNames.RemoveAt( selectedIndex );
            _queuedScenario2Commands.Remove( scenarioName );
            
            var usedParameterNames = new HashSet<string>();
            foreach( var commands in _queuedScenario2Commands.Values )
                foreach( var command in commands )
                    usedParameterNames.UnionWith( command.Item.GetParameterNames() );
            
            var removeUnusedParameters = false;
            if( usedParameterNames.Count != _inputParameters.Count )
                if( MessageBox.Show("Remove unused input parameters?", "Attention", MessageBoxButtons.YesNo ) == DialogResult.Yes )
                    removeUnusedParameters = true;
            
            if( removeUnusedParameters )
            {
                _inputParameters = _inputParameters.Where( e => usedParameterNames.Contains( e.Name ) ).ToList();
                
                OnSetInputParametersDataGrid( _inputParameters, -1 );
            }
            
            OnSetQueuedScenarios( _queuedScenarioNames.Select( e => e.Item ).ToList() );
            if( _queuedScenarioNames.Count > 0 )
                OnSetQueuedScenarioIndex( 0 );
        }

        public string[] GetAvailableUnusedScenarios()
        {
            var files = GetFileNames( Settings.FolderForScenarios, Settings.ScenarioFileExtension );

            var allScenarioNames = files.Select( fileName => GetScenarioName( fileName, Settings.FolderForScenarios, Settings.ScenarioFileExtension ) ).ToArray();

            return allScenarioNames.Where( name => _queuedScenarioNames.All( q => q.Item != name ) ).ToArray();
        }

        public void AddScenario( string scenarioName )
        {
            if( string.IsNullOrEmpty( scenarioName.Trim() ) )
                throw new Exception("The scenario name can't be empty");

            var fileName                        = Path.Combine(Settings.FolderForScenarios, $"{scenarioName}.{Settings.ScenarioFileExtension}" );
            var addedScenarioParameterNames     = new HashSet<string>();
            try
            {
                var lines = File.ReadLines( fileName ).ToArray();
                    
                var commands = _compiler.GetCompiledCommands( lines );

                foreach( var command in commands )
                    addedScenarioParameterNames.UnionWith( command.GetParameterNames() );
                    
                _queuedScenario2Commands[ scenarioName ] = commands.Select( e => new IsExecutedWrapper<ICommand>( e ) ).ToList();
                _queuedScenarioNames.Add( new IsExecutedWrapper< string >(scenarioName));
            }
            catch( Exception e )
            {
                MessageBox.Show( $"{fileName} - {e.Message}", "Error in the scenario file" );
            }

            OnSetQueuedScenarios( _queuedScenarioNames.Select( e => e.Item ).ToList() );
            OnSetQueuedScenarioIndex( _queuedScenarioNames.FindIndex( e => e.Item == _currentQueuedScenarioName ) );
            
            AddNewInputParameters( addedScenarioParameterNames );
        }

        private void AddNewInputParameters( HashSet< string > parameterNames )
        {
            var initialInputParametersCount = _inputParameters.Count;
            foreach( var parameterName in parameterNames )
                if( _inputParameters.All( e => e.Name != parameterName ) )
                    _inputParameters.Add( new Parameter( parameterName, string.Empty ) );

            if( initialInputParametersCount != _inputParameters.Count )
                OnSetInputParametersDataGrid( _inputParameters, initialInputParametersCount );
        }

        public void LoadConfig( string fileName )
        {
            try
            {
                var config = Serializer.DeserializeFromFile<InputConfig>( fileName );
                Reset();

                var scenarioFileNames = config.Scenarios.Select( e => Path.Combine( Settings.FolderForScenarios, $"{e}.{Settings.ScenarioFileExtension}" )).ToArray();

                LoadScenarios( scenarioFileNames );
                
                foreach( var pair in config.Parameters )
                {
                    var existingParameter = _inputParameters.FirstOrDefault( e => e.Name == pair.Key );
                    
                    if( existingParameter is null )
                        _inputParameters.Add( new Parameter( pair.Key, pair.Value ) );
                    else
                        existingParameter.Value = pair.Value;
                }
                
                OnSetInputParametersDataGrid( _inputParameters, _inputParameters.Count > 0 ? 0 : -1 );
                
                OnSetConfigFileName( fileName );
            }
            catch( Exception e )
            {
                MessageBox.Show( e.Message, "Error" );
            }
            
        }

        public void SaveConfig( string fileName )
        {
            try
            {
                var outputConfig = new OutputConfig( 
                    new InputConfig(
                        _queuedScenarioNames.Select( e => e.Item ).ToArray(), 
                        _inputParameters.ToDictionary( pair => pair.Name, pair => pair.Value ) ), 
                    _scenarioName2OutputParameters);
            
                Serializer.SerializeToFile( outputConfig, fileName );
                
                OnSetConfigFileName( fileName );
            }
            catch( Exception e )
            {
                MessageBox.Show( e.Message, "Error" );
            }
        }
    }
}