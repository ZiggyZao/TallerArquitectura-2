using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OrderWebApi.Models;
using System.Data;

namespace OrderWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Orders> _orderCollection;
        public OrderController() {

            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var connectionString = $"mongodb://{dbHost}:27017/{dbName}";


            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            _orderCollection = database.GetCollection<Orders>("orders");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> GerOrder()
        {
            return await _orderCollection.Find(Builders<Orders>.Filter.Empty).ToListAsync();
        }

        [HttpGet("orderId")]
        public async Task<ActionResult<Orders>> GerById(string orderId)
        {
            var filterDefinition = Builders<Orders>.Filter.Eq(x => x.OrderId, orderId);
            
            return await _orderCollection.Find(filterDefinition).SingleAsync();
        }


        [HttpPost]
        public async Task<ActionResult> Create(Orders order)
        {
            await _orderCollection.InsertOneAsync(order);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Modify(Orders order)
        {
            var filterDefinition = Builders<Orders>.Filter.Eq(x => x.OrderId, order.OrderId);
            await _orderCollection.ReplaceOneAsync(filterDefinition, order);
            return Ok();
        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult> Delete(string order)
        {
            await _orderCollection.DeleteOneAsync(order);
            return Ok();
        }





    }
}
