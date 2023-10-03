using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogVarellaMotos.Framework
{
    public static class Helper
    {
        public static string ConcatUrlWithDiretorio(string texto)
        {
            //return string.Format("/blog{0}", texto);
            return string.Format("{0}", texto);
        }
    }
}