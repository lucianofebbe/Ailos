using AilosInfra.Bases.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses.Movimento
{
    public record InitMovimentoResponse : BaseResponse
    {
        public Guid Guid { get; set; }
        public Guid NumeroDaConta { get; set; }
        public DateTime DataMovimento { get; set; }
        public decimal Valor { get; set; }
    }
}
