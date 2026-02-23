using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ReflectionApp
{

	class TableNameAttribute : Attribute
	{
		public string Name{get;set;}

		public TableNameAttribute(string name)
		{
			Name = name;
		}
	}
	class ColumnNameAttribute : Attribute
	{
		public string Name{get;set;}
		public ColumnNameAttribute(string name)
		{
			Name = name;
		}
	}

	class Person
	{
		[ColumnName("Jmeno")]
		public string Name { get; set; }

		public int Age { get; set; }
	}

	[TableName("Auto")]
	class Car
	{
		public int Speed { get; set; }
	}
	public class Program
	{

		public static string CreateInsertSql(object obj)
		{
			Type type = obj.GetType();
			string tableName = type.GetCustomAttribute<TableNameAttribute>()?.Name ?? type.Name;
			string sql = "INSERT INTO " + tableName + " (";
			sql += string.Join(", ", type.GetProperties().Select(x => {
				ColumnNameAttribute attr = x.GetCustomAttribute<ColumnNameAttribute>();
				return attr?.Name ?? x.Name;
				}));
			sql += ") VALUES (";
			sql += string.Join(", ", type.GetProperties().Select(x =>
			{
				object val = x.GetValue(obj);
				if(val.GetType() == typeof(string))
				{
					return $"\"{val}\"";
				}
				return val;
			}));
			sql += ")";
			return sql;
		}
		public static void Main(string[] args)
		{
			Console.WriteLine(CreateInsertSql(new Person()
			{
				Name = "Jan",
				Age = 20
			}));
			Console.WriteLine(CreateInsertSql(new Car()
			{
				Speed = 100
			}));

			string baseDir = Path.GetFullPath("../../../VSB-CSHARP2/Reflection/ReflectionApp/plugins/Debug/net9.0");
			string[] dlls = Directory.GetFiles(baseDir, "*.dll");

			string rectangleDllPath = Path.Combine(baseDir, "Rectangle.dll");

			Assembly assembly = Assembly.LoadFile(rectangleDllPath);


			Assembly[] assemblies = dlls.Select(x => Assembly.LoadFile(x)).ToArray();

			Type[] types = assemblies.SelectMany(x => x.GetTypes()).Where(x => x.Name.EndsWith("Calculator")).ToArray();

			for (int i = 0; i < types.Length; i++)
			{
				Console.WriteLine($"{i}. {types[i].Name}");
			}

			int index = int.Parse(Console.ReadLine());

			Assembly assembly1 = types[index].Assembly;

			Type calcType = types[index];
			// Type[] types = assembly.GetTypes();
			// foreach(Type item in types)
			// {
			// 	Console.WriteLine(item.FullName);
			// }

			// foreach(MethodInfo item in calcType.GetMethods())
			// {
			// 	Console.WriteLine(item.ReturnType + " " + item.Name);
			// 	foreach(ParameterInfo info in item.GetParameters())
			// 	{
			// 		Console.WriteLine(" - " + info.ParameterType + " " + info.Name);
			// 	}
			// }

			// Console.WriteLine(" ----------------- ");

			// foreach(FieldInfo fieldInfo in calcType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
			// {
			// 	Console.WriteLine(fieldInfo.FieldType + " " + fieldInfo.Name);
			// }
			// Type calcType = assembly.GetType("Rectangle.RectangleCalculator");

			// object instance = assembly.CreateInstance("Rectangle.RectangleCalculator");
			object instance = Activator.CreateInstance(calcType);

			foreach (PropertyInfo item in calcType.GetProperties())
			{
				Console.WriteLine(item.Name);
				double tmp = double.Parse(Console.ReadLine());
				item.SetValue(instance, tmp);
			}

			MethodInfo mi = calcType.GetMethod("GetArea", new Type[0]);
			double result = (double)mi.Invoke(instance, new object[0]);

			Type stringType = typeof(string);
			MethodInfo mi2 = calcType.GetMethod("GetArea", new Type[] { stringType });
			string result2 = (string)mi2.Invoke(instance, new object[] { "cm" });

			Console.WriteLine(result2);
		}



	}
}