using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorQuiz.Shared.ViewModels
{
    public class MediaViewModel
    {
        public string Type { get; set; }
        public string Path { get; set; }

    }
}
