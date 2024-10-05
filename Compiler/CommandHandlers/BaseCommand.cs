using System;
using System.Linq;

namespace ScenarioBasedVideoUploader.Compiler.CommandHandlers
{
    public interface ICommand
    {
        void Process( IContext context );
        

        string   GetLogMessage( IContext context );

        string[] GetArgumentValues();//значения всех аргументов, переданных при создании команды
        string[] GetParameterNames();//только те значения аргументов, что представляют собой параметры (т.е. содержат фигурные скобки)  
    }
    
    //NB нет смысла указывать ICommand
    public abstract class BaseCommand
    {
        private readonly string _commandName;
        
        protected BaseCommand( string commandName )
        {
            if( string.IsNullOrEmpty(commandName) )
                throw new ArgumentException( $"{nameof(commandName)}" );
            
            _commandName   = commandName;
        }
        
        protected string CreateLogMessage( IContext context, params string[] arguments )
        {
            var parameterValuesInContext = arguments is not null 
                ? string.Join( ", ", arguments.Select( e => $"\"{e.InContext( context )}\"" ) ) 
                : string.Empty;

            return $"{_commandName}({parameterValuesInContext})";
        }
     
        public string[] GetParameterNames()
        {
            return InterpolationProcessor.GetStringsWithParameters( GetArgumentValues() );
        }
        
        public string GetLogMessage( IContext context )
        {
            return CreateLogMessage( context, GetArgumentValues() );
        }

        public override string ToString()
        {
            var arguments = string.Join(", ", GetArgumentValues().Select( e => $"\"{e}\"" ) );
            
            if( arguments.Length > 0 )
                return $"{_commandName}( {arguments} )";

            return $"{_commandName}()";
        }

        public abstract string[] GetArgumentValues();
    }
}