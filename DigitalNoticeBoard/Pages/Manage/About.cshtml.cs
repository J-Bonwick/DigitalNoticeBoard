using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitalNoticeBoard.Pages.Manage
{
    public class AboutModel : PageModel
    {
        public string AssemblyVersion { get; set; }
        public void OnGet()
        {
            AssemblyVersion = typeof(RuntimeEnvironment).GetTypeInfo()
            .Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
        }
    }
}
