using MyBoard.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.Models.BoardModels
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Card> Cards { get; set; }
        virtual public ICollection<UserBoard> Users { get; set; }
    }
}
