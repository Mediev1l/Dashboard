using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.Models.BoardModels
{
    public class Card
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreationDate { get; set; }
        [ForeignKey("Board")]
        public int BoardId { get; set; }
        public Board Board { get; set; }

    }
}
