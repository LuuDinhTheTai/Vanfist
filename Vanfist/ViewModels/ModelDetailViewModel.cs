using System.Collections.Generic;
using Vanfist.Entities;

namespace Vanfist.ViewModels
{
    public class ModelDetailViewModel
    {
        public Model Model { get; set; } = default!;
        public List<Attachment> Attachments { get; set; } = new();
    }
}
