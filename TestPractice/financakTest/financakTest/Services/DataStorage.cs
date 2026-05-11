using System.Reflection;

namespace financakTest.Services
{
    public class DataStorage
    {
        public void SaveToFile(object anyObject, string path)
        {
            var objectType = anyObject.GetType();

            using(StreamWriter sw = new StreamWriter(path))
            {
                foreach (PropertyInfo property in objectType.GetProperties())
                {
                    var propertyName = property.Name;
                    var value = property.GetValue(anyObject);
                    sw.WriteLine($"#{propertyName} => {value};");
                }
                sw.WriteLine("");
            }
        }
    }
}
