using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ScenarioBasedVideoUploader
{
    public static class Serializer
    {
        public static T DeserializeFromFile<T>( string fileName )
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            
            return JsonSerializer.Deserialize< T >( File.ReadAllText( fileName ), options ) ?? throw new Exception( $"Can't deserialize file: {fileName}" );
        }

        public static void SerializeToFile<T>( T data, string fileName, Encoding? encoding = null)
        {
            var serialized = Serialize( data );
            
            File.WriteAllText( fileName, serialized, encoding ?? Encoding.UTF8 );
        }

        public static string Serialize<T>( T data )
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            
            return JsonSerializer.Serialize( data, options );
        }

        public static T Deserialize< T >( string data )
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            return JsonSerializer.Deserialize<T>( data, options ) ?? throw new Exception( $"Can't deserialize data" );
        }
    }
}