using Microsoft.VisualStudio.TestPlatform.Utilities.Helpers;
using System.Diagnostics;
using PlatesAvaloniaProject.Helpers;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string file1 = @"D:\DemoCode\JSONDemo\JSON\JSON\bin\Debug\netcoreapp3.1\JSON.dll";
            string file2 = @"D:\DemoCode\JSONDemo\JSON\JSON\bin\Debug\netcoreapp3.1\JSON - ¸±±¾.dll";
            var a = new FileInfoHelper(new FileInfo(file1));
            var b = a.Equals(new FileInfo(file2));

            var c = new FileInfo(file1).Equals(new FileInfo(file2));
        }
    }
}