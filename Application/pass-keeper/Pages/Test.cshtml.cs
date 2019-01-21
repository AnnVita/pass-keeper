using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FtpClient;

namespace pass_keeper.Pages
{
    public class TestModel : PageModel
    {        
        public string Result { get; set; }

        public string StringToCopy { get; set; }

        public string Explanation { get; set; }

        [BindProperty]
        public string HostName { get; set; }

        [BindProperty]
        public int Port { get; set; }
        
        [BindProperty]
        public string Login { get; set; }
        
        [BindProperty]
        public string Password { get; set; }

        public void OnPost()
        {
            string uri = "ftp://" + HostName ;
            string login = Login;
            string password = Password;
            
            Client client = new Client(uri, login, password);
            string answer = "";
            try
            {
                answer = client.PrintWorkingDirectory();
                Result = answer;
                StringToCopy = $"==== FTP access to {HostName} ==== {Environment.NewLine} {Environment.NewLine} uri : {uri} {Environment.NewLine} host : {HostName} {Environment.NewLine} port : {Port} {Environment.NewLine} user : {Login} {Environment.NewLine} password : {Password}";
                Explanation = "Все ОК :)";
            }
            catch
            {
                Result = "ERROR HAS OCCURED";
                StringToCopy = $"==== FTP access to {HostName} ==== {Environment.NewLine} {Environment.NewLine} uri : {uri} {Environment.NewLine} host : {HostName} {Environment.NewLine} port : {Port} {Environment.NewLine} user : {Login} {Environment.NewLine} password : {Password}";
                Explanation = "Доступы не актуальны или невалидны :( ";
            }

        }

    }
}