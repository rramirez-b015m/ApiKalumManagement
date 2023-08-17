
namespace KalumManagement.Dtos
{
    public class PaginationResponse<T> : PaginacionDTO<T>
    {
        public PaginationResponse(List<T> source, int number, int registros)
        {
            this.Number = number;
            int cantidadRegistrosPorPagina = 5;
            int totalRegistros = registros;
            this.TotalPages = (int)Math.Ceiling((double)totalRegistros / cantidadRegistrosPorPagina);
            this.Content = source;
            if (this.Number == 0)
            {
                this.First = true;
            }
            else if ((this.Number + 1) == this.TotalPages)
            {
                this.Last = true;
            }
        }
    }
}