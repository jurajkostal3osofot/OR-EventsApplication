using Shared.Models;

namespace OREventApp.Models
{
    class CellModel
    {
        public long Id { get; set; }
        public string Heading { get; set; }
        public string MiniMap { get; set; }
        public int NumberOfAttendates { get; set; }
        public EventShared EventShared { get; set; }
    }
}
