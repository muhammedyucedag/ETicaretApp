﻿using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.Enums;
using ETicaretAPI.Application.Features.Commands.Basket.AddItemToBasket;
using ETicaretAPI.Application.Features.Commands.Basket.RemoveBasketItem;
using ETicaretAPI.Application.Features.Commands.Basket.UpdateQuantity;
using ETicaretAPI.Application.Features.Queries.Basket.GetBasketItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class BasketsController : ControllerBase
    {
        readonly IMediator mediator;

        public BasketsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstans.Baskets, ActionType = ActionType.Reading, Definition = "Get Basket Items")]
        public async Task<IActionResult> GetBasketItems([FromQuery] GetBasketItemsQueryRequest getBasketItemsQueryRequest)
        {
            List<GetBasketItemsQueryResponse> response = await mediator.Send(getBasketItemsQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstans.Baskets, ActionType = ActionType.Writing, Definition = "Add Item to Basket")]
        public async Task<IActionResult> AddItemtoBasket(AddItemToBasketCommandRequest addItemToBasketCommandRequest)
        {
            AddItemToBasketCommandResponse response = await mediator.Send(addItemToBasketCommandRequest);
            return Ok(response);
        }

        [HttpPut]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstans.Baskets, ActionType = ActionType.Updating, Definition = "Update Quantity")]
        public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest updateQuantityCommandHandlerRequest)
        {
            UpdateQuantityCommandResponse response = await mediator.Send(updateQuantityCommandHandlerRequest);
            return Ok(response);
        }

        [HttpDelete ("{BasketItemId}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstans.Baskets, ActionType = ActionType.Deleting, Definition = "Remove Basket Items")]
        public async Task<IActionResult> RemoveBasketItem([FromRoute] RemoveBasketItemCommandRequest removeBasketItemCommandRequest)
        {
            RemoveBasketItemCommandResponse response = await mediator.Send(removeBasketItemCommandRequest);
            return Ok(response);
        }
    }
}
