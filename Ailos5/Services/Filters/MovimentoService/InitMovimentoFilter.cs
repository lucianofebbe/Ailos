using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Filters.MovimentoService
{
    public record InitMovimentoFilter
    {
        public Guid NumeroContaCorrente { get; set; }
        public decimal Valor { get; set; }
        public char TipoDeMovimento { get; set; }
    }
}
