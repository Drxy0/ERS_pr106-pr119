using ERS_pr106_pr119.FileReader;
using System.Configuration;

namespace ERS_pr106_pr119Test
{
	public class FilesDirectoryTest
	{
		private FilesDirectory instance { get; set; } = null;
		[SetUp] public void SetUp()
		{
			instance = new FilesDirectory();
		}


		[Test]
		public void GetFilesDirectoryIsNull()
		{
			//Assign
			string? filesDirectory = null;
			//Act
			var files = instance.GetFiles();

			//Assert
			Assert.IsNull(files);
		}

		[Test]
		public void GetFilesWrongFolder()
		{
			//Assign
			string? filesDirectory = "C:\\test";
			//Act
			var files = instance.GetFiles();

			//Assert
			Assert.IsNull(files);
		}
	}
}