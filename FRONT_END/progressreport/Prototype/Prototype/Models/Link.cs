using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prototype.Models
{
    public class Link
    {
        public int NewsId { get; set; }
        public string MainHeading { get; set; }
        public string AuthorInformation { get; set; }
        public string BodyText { get; set; }
    }
}