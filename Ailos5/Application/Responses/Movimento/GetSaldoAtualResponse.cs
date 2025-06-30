using AilosInfra.Bases.Dtos;

namespace Application.Responses.Movimento
{
    public record GetSaldoAtualResponse : BaseResponse
    {
        public DateTime DataMovimento { get; set; }
        public decimal Valor { get; set; }
    }
}
