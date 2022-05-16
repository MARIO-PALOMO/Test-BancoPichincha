using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace banco_dto.Entidades
{
    [Table("Movimiento")]
    public class Movimiento
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime Fecha { get; set; }

        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }

        [ForeignKey("Cuenta")]
        public int CuentaId { get; set; }
        public virtual Cuenta Cuenta { get; set; }

    }
}
