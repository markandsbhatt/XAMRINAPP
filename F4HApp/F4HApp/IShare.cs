using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace F4HApp
{
    public interface IShare
    {
        void Share(string subject, string message, ImageSource image);
    }
}
