using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using src.Model;

namespace TimerWeb.Pages
{
    public class IndexModel : PageModel
    {
        public ShortBreak _shortBreak;

        [BindProperty]
        public string CurrentTime { get => _shortBreak.currenTime; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, ShortBreak shortBreak)
        {
            _logger = logger;

            _shortBreak =  shortBreak;
        }
 
        public IActionResult OnGetStart()
        {
            _shortBreak.Start();

            return Page();
        }

         public IActionResult OnGetStop()
        {
            _shortBreak.Stop();
            
            return Page();
        }

        public IActionResult OnGetRestart()
        {
            _shortBreak.Restart();

            return Page();
        }

        public async Task Get()
        {
            
            string[] data = new string[]  {
                    "Hello World!",
                    "Hello Galaxy!",
                    "Hello Universe!"
            };

            Response.Headers.Add("Content-Type","text/event-stream");

            foreach (var item in data)
            {
                await Task.Delay(TimeSpan.FromSeconds(4));

                string dataItem      = $"data: {item}\n\n";

                byte[] dataItemBytes = ASCIIEncoding.ASCII.GetBytes(dataItem);
                
                await Response.Body.WriteAsync(dataItemBytes,0,dataItemBytes.Length);

                await Response.Body.FlushAsync(); 
            }
        } 

        public void OnGet()
        {

        }
    }
}
