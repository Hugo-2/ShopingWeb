using Dapper;
using GoMarket.Models;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace GoMarket.Servicios
{
    public interface IRepositorioListaCompras
    {
        Task BorrarListaRecordatorio(int id);
        Task<bool> EditarListaRecordatorio(ProductosVM producto);
        Task<bool> EditarProductoTicket(TicketProductVM producto);
        Task<ProductosVM> GetListaEditar(int id);
        Task<IEnumerable<NombreDeTiendasVM>> GetNombreTiendas();
        Task<TicketProductVM> GetProductoEditar(int id);
        Task<IEnumerable<TicketProductVM>> GetProductosComprar(string tienda);
        Task<IEnumerable<ProductosVM>> GetRecordarCompra();
        Task<bool> InsertNewProduct(ProductosVM producto);
        Task<bool> InsertNewProductTicket(TicketProductVM producto);
    }
    public class RepositorioListaCompras : IRepositorioListaCompras
    {
        private readonly string cadConexion;
        public RepositorioListaCompras(IConfiguration configuration)
        {
            cadConexion = configuration.GetConnectionString("CadenaConexion_GoMarket");
        }


        /* -------------------------------------------------------------------------------------------- */


        public async Task<IEnumerable<NombreDeTiendasVM>> GetNombreTiendas()
        {
            using var conexion = new SqlConnection(cadConexion);
            var tiendas = await conexion.QueryAsync<NombreDeTiendasVM>(@"SELECT tienda as NombreTienda, COUNT(*) as Cantidad 
                                                                         FROM T_ListaCompras GROUP BY tienda;");
            return tiendas;
        }

        public async Task<IEnumerable<ProductosVM>> GetRecordarCompra()
        {
            using var conexion = new SqlConnection(cadConexion);
            var recordar = await conexion.QueryAsync<ProductosVM>(@"Select id_listaCompras as Id, producto, cantidad, tienda
                                                                    from T_ListaCompras
                                                                    Order by tienda, producto asc");
            return recordar;   
        }

        public async Task<ProductosVM> GetListaEditar(int id)
        {
            using var conexion = new SqlConnection(cadConexion);
            var producto = await conexion.QueryFirstOrDefaultAsync<ProductosVM>(@"Select id_listaCompras as Id, producto, cantidad, tienda
                                                                                      From T_ListaCompras
                                                                                      Where id_listaCompras = @Id",
                                                                                      new { id }
                                                                                    );
            return producto;
        }

        public async Task<bool> InsertNewProduct(ProductosVM producto)
        {
            using var conexion  = new SqlConnection(cadConexion);
            var regisrado = await conexion.QuerySingleAsync<bool>(@"spAddNewProduct",
                                                                   new {
                                                                       producto = producto.Producto,
                                                                       cantidad = producto.Cantidad,
                                                                       tienda = producto.Tienda
                                                                   },
                                                                   commandType: System.Data.CommandType.StoredProcedure);
            return regisrado;
        }

        public async Task<bool> EditarListaRecordatorio(ProductosVM producto)
        {
            using var conexion = new SqlConnection(cadConexion);
            var editado = await conexion.QuerySingleAsync<bool>(@"spUpdateProduct", 
                                                            new { 
                                                                id = producto.Id,
                                                                producto = producto.Producto,
                                                                cantidad= producto.Cantidad,
                                                                tienda = producto.Tienda
                                                            },
                                                            commandType: System.Data.CommandType.StoredProcedure);
            return editado;
        }

        public async Task BorrarListaRecordatorio(int id)
        {
            using var conexion = new SqlConnection(cadConexion);
            var borrado = await conexion.QueryAsync(@"DELETE FROM T_ListaCompras WHERE id_listaCompras = @Id;", new { id });
        }

        /* ------------ TICKET -------------------------------------------------------------------------------- */

        public async Task<IEnumerable<TicketProductVM>> GetProductosComprar(string tienda)
        {
            using var conexion = new SqlConnection(cadConexion);
            var productos = await conexion.QueryAsync<TicketProductVM>(@"Select id_listaCompras as id, producto, cantidad, precio, (cantidad * precio) as subTotal, tienda 
                                                                         From T_ListaCompras
                                                                         Where tienda = @Tienda
                                                                         Order by producto asc",
                                                                         new { tienda }
                                                                       );
            return productos;
        }

        public async Task<TicketProductVM> GetProductoEditar(int id)
        {
            using var conexion = new SqlConnection(cadConexion);
            var producto = await conexion.QueryFirstOrDefaultAsync<TicketProductVM>(@"Select id_listaCompras as Id, producto, cantidad, precio, tienda
                                                                                      From T_ListaCompras
                                                                                      Where id_listaCompras = @Id",
                                                                                      new { id }
                                                                                    );
            return producto;
        }

        public async Task<bool> InsertNewProductTicket(TicketProductVM producto)
        {
            using var conexion = new SqlConnection(cadConexion);
            var regisrado = await conexion.QuerySingleAsync<bool>(@"spAddNewProductTicket",
                                                                   new
                                                                   {
                                                                       producto = producto.Producto,
                                                                       cantidad = producto.Cantidad,
                                                                       precio = producto.Precio,
                                                                       tienda = producto.Tienda
                                                                   },
                                                                   commandType: System.Data.CommandType.StoredProcedure);
            return regisrado;
        }

        public async Task<bool> EditarProductoTicket(TicketProductVM producto)
        {
            using var conexion = new SqlConnection(cadConexion);
            var editado = await conexion.QuerySingleAsync<bool>(@"spUpdateTicketProduct",
                                                            new
                                                            {
                                                                id = producto.Id,
                                                                producto = producto.Producto,
                                                                cantidad = producto.Cantidad,
                                                                precio = producto.Precio
                                                            },
                                                            commandType: System.Data.CommandType.StoredProcedure);
            return editado;
        }

    }
}
