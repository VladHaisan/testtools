using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace FCMMessageSender
{
	class Program
	{
		static void Main(string[] args)
		{
			//Testrequest();
			Console.WriteLine(SendNotificationFromFirebaseCloud()); ;
			Console.ReadKey();
		}
		public static String SendNotificationFromFirebaseCloud()
		{
			var result = "-1";
			var webAddr = "https://fcm.googleapis.com/fcm/send";
			string applicationID = "AIzaSyBucw4B6mTvszl1EJsKCRu2afyjfd9G3c8";
			string deviceId = "cF1JFR5I-AI:APA91bGK74bZaS1QklQvSSUQ1P7CcKZ3YdLwvgpTgq75w00tOhKPMs8IHH3iGm6mEOTb26qHNiZCLrRLvyCgsCHOuk4GbzsMaN1LEi8CmPvc8T4LsThDFpnzcuDUkU-90IZUMkQL_zM5";
			string senderId = "953128020008";

			var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
			httpWebRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
			httpWebRequest.Method = "POST";

			using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
				//string json = JsonConvert.SerializeObject(new {
				//    to = deviceId,
				//    notification = new
				//    {
				//        body = "datatest",
				//        title = "atatestttitle",
				//        sound = "Enabled"
				//    }
				//});
				string json = JsonConvert.SerializeObject(new
				{
					to = deviceId,
					data = new
					{
						CreatedDate = DateTime.UtcNow.ToString(),
						AlertType = "AlertType",
						NotificationUrl = @"https://www.ex.ua"
                    }
				});


				streamWriter.Write(json);
				streamWriter.Flush();
			}

			var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				result = streamReader.ReadToEnd();
			}

			return result;
		}
		public static void Testrequest()
		{
			var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://requestb.in/1noc5vo1");
			httpWebRequest.Headers.Add("Message:" + "Syp");
			httpWebRequest.Method = "POST";
			var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			//using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
			//{
			//    return streamReader.ReadToEnd();
			//}
		}
	}
}
