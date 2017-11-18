using System.Collections.Generic;
using GigHubWebApp.Models;

namespace GigHubWebApp.ViewModels {
    public class GigFormViewModel {
        public string Venue { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}