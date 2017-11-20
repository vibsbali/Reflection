using System;
using System.Reflection;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            Basics();

            var point = new Point(5,5);

            var mirroredPoint = new Mirror(point.GetType());
            mirroredPoint.DumpType();
            mirroredPoint.DumpProperties();

            var mirroredObject = new Mirror(typeof(System.Int32));
            mirroredObject.DumpType();
        }

        private static Point Basics()
        {
            //Loading type using they type itself
            var ptType = typeof(Point);
            Console.WriteLine(ptType);

            //Loading type using the object
            var point = new Point(5, 5);
            var ptTypeObj = point.GetType();
            Console.WriteLine(ptTypeObj);

            //Loading type using the static mehtod by string literal
            var loadedType = System.Type.GetType("Reflection.Point");
            Console.WriteLine(loadedType);

            var ptAssem = ptType.Assembly;
            Console.WriteLine(ptAssem.FullName);
            Console.WriteLine(ptAssem.CodeBase);

            //Assemblies are made up of modules and modules are in term host types
            //Generally each Assembly as one Module 
            foreach (var ptAssemModule in ptAssem.Modules)
            {
                Console.WriteLine(ptAssemModule);
            }
            return point;
        }
    }

    //This class will act as Ildasm
    public class Mirror
    {
        public Mirror(Type type)
        {
            MirroredType = type;
        }

        public Type MirroredType { get; set; }


        public void DumpType()
        {
            Console.WriteLine($"{MirroredType.Name} loaded from {MirroredType.Assembly.FullName}");
            Console.WriteLine($" Inherits from {MirroredType.BaseType.Name}");
            Console.WriteLine($" implements:");
            foreach (var @interface in MirroredType.GetInterfaces())
            {
                Console.WriteLine($"        {@interface.Name}");
            }
        }


        public void DumpProperties()
        {
            Console.WriteLine("Properties:");
            var flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            foreach (var propertyInfo in MirroredType.GetProperties(flags))
            {
                Console.WriteLine($" {propertyInfo.Name} : {propertyInfo.PropertyType}");
                Console.WriteLine($"   r: {propertyInfo.CanRead}, w:{propertyInfo.CanWrite}");
            }
        }
    }

    
}
