using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyBoard.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.Models.BoardModels
{
    public class UserBoard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BoardId { get; set; }
        [ForeignKey("BoardId")]
        public Board Board { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser User{ get; set; }
    }
}
