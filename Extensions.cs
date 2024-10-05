using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ScenarioBasedVideoUploader
{
    public static class Extensions
    {
        public static IEnumerable<string> Select( this ListBox.ObjectCollection collection, Func<object, string> func )
        {
            foreach( var obj in collection )
                yield return func( obj );
        }
        
        public static void AddRange( this ListBox.ObjectCollection collection, IEnumerable<string> items )
        {
            foreach( var item in items )
                collection.Add( item );
        }
    }
}