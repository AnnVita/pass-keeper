using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace FtpClient
{
	public class Client {
		private string password;
		private string userName;
		private string uri;
		private int bufferSize = 1024;
 
		public bool Passive = true;
		public bool Binary = true;
		public bool EnableSsl = false;
		public bool Hash = false;
		
		public Client(string uri, string userName, string password) {
			this.uri = uri;
			this.userName = userName;
			this.password = password;
		}
 
		public string ChangeWorkingDirectory(string path) {
			uri = combine(uri, path);
 
			return PrintWorkingDirectory();
		}
 
		public string DeleteFile(string fileName) {
			var request = createRequest(combine(uri, fileName), WebRequestMethods.Ftp.DeleteFile);
			
			return getStatusDescription(request);
		}
 
		public string DownloadFile(string source, string dest) {
			var request = createRequest(combine(uri, source), WebRequestMethods.Ftp.DownloadFile);
			
			byte[] buffer = new byte[bufferSize];
 
			using (var response = (FtpWebResponse)request.GetResponse()) {
				using (var stream = response.GetResponseStream()) {
					using (var fs = new FileStream(dest, FileMode.OpenOrCreate)) {
						int readCount = stream.Read(buffer, 0, bufferSize);
 
						while (readCount > 0) {
							if (Hash)
								Console.Write("#");
 
							fs.Write(buffer, 0, readCount);
							readCount = stream.Read(buffer, 0, bufferSize);
						}
					}
				}
 
				return response.StatusDescription;
			}
		}
 
		public DateTime GetDateTimestamp(string fileName) {
			var request = createRequest(combine(uri, fileName), WebRequestMethods.Ftp.GetDateTimestamp);
			
			using (var response = (FtpWebResponse)request.GetResponse()) {
				return response.LastModified;
			}
		}
 
		public long GetFileSize(string fileName) {
			var request = createRequest(combine(uri, fileName), WebRequestMethods.Ftp.GetFileSize);
			
			using (var response = (FtpWebResponse)request.GetResponse()) {
				return response.ContentLength;
			}
		}
 
		public string[] ListDirectory() {
			var list = new List<string>();
 
			var request = createRequest(WebRequestMethods.Ftp.ListDirectory);
			
			using (var response = (FtpWebResponse)request.GetResponse()) {
				using (var stream = response.GetResponseStream()) {
					using (var reader = new StreamReader(stream, true)) {
						while (!reader.EndOfStream) {
							list.Add(reader.ReadLine());
						}
					}
				}
			}
 
			return list.ToArray();
		}
 
		public string[] ListDirectoryDetails() {
			var list = new List<string>();
 
			var request = createRequest(WebRequestMethods.Ftp.ListDirectoryDetails);
			
			using (var response = (FtpWebResponse)request.GetResponse()) {
				using (var stream = response.GetResponseStream()) {
					using (var reader = new StreamReader(stream, true)) {
						while (!reader.EndOfStream) {
							list.Add(reader.ReadLine());
						}
					}
				}
			}
 
			return list.ToArray();
		}
 
		public string MakeDirectory(string directoryName) {
			var request = createRequest(combine(uri, directoryName), WebRequestMethods.Ftp.MakeDirectory);
			
			return getStatusDescription(request);
		}
 
		public string PrintWorkingDirectory() {
			var request = createRequest(WebRequestMethods.Ftp.PrintWorkingDirectory);
 
			return getStatusDescription(request);
		}

		private FtpWebRequest createRequest(string uri, string method) {
			var r = (FtpWebRequest)WebRequest.Create(uri);
 
			r.Credentials = new NetworkCredential(userName, password);
			r.Method = method;
			r.UseBinary = Binary;
			r.EnableSsl = EnableSsl;
			r.UsePassive = Passive;
 
			return r;
		}
        
		private FtpWebRequest createRequest(string method) {
			return createRequest(uri, method);
		}
 
		private string getStatusDescription(FtpWebRequest request) {
			using (var response = (FtpWebResponse)request.GetResponse()) {
				return response.StatusDescription;
			}
		}
 
		private string combine(string path1, string path2) {
			return Path.Combine(path1, path2).Replace("\\", "/");
		}
	}
}