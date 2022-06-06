using Cards.api.Data;
using Cards.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cards.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly CardDbContext cardDbContext;

        public CardsController(CardDbContext cardDbContext)
        {
            this.cardDbContext = cardDbContext;
        }

        //get all cards details
        [HttpGet]
       public async Task<IActionResult> GetAllCards()
        {
           var cards = await cardDbContext.Cards.ToListAsync();
            return Ok(cards);
        }


        //get card detail
        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute]Guid id)
        {
            var card = await cardDbContext.Cards.FirstOrDefaultAsync(x=>x.Id == id);
            if (card != null)
            {
                return Ok(card);
            }
            return NotFound("Card Not Found");
        }

        //add card detail
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            card.Id = Guid.NewGuid();
            await cardDbContext.Cards.AddAsync(card);
            await cardDbContext.SaveChangesAsync();
            //2 o 1 response
            return CreatedAtAction(nameof(GetCard),new {id = card.Id },card);

        }

        //update card detail
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id,[FromBody] Card card)
        {
            var existingCard = await cardDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null)
            {
                existingCard.CardHolderName = card.CardHolderName;
                existingCard.CardNumber = card.CardNumber;
                existingCard.CVV = card.CVV;
                existingCard.ExpiryMonth = card.ExpiryMonth;
                existingCard.ExpiryYear = card.ExpiryYear;
                await cardDbContext.SaveChangesAsync();
                return Ok(existingCard);
            }
            return NotFound("Card Not Found");
        }

        //Delete card detail
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var existingCard = await cardDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null)
            {
                cardDbContext.Remove(existingCard);
                await cardDbContext.SaveChangesAsync();
                return Ok(existingCard);
            }
            return NotFound("Card Not Found");
        }
    }
}
