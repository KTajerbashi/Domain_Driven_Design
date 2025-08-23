using BaseSource.Core.Application.UseCases.Store.Products.Handlers.Commands.Create;
using BaseSource.Core.Application.UseCases.Store.Products.Handlers.Queries.GetById;
using BaseSource.EndPoint.WebApi.Common.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.EndPoint.WebApi.Controllers.Store;

public class ProductController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateCommand command)
    {
        return await CreateAsync<ProductCreateCommand, ProductCreateResponse>(command);
    }
    
    [HttpGet("{entityId}")]
    public async Task<IActionResult> GetById(Guid entityId)
    {
        return await GetByIdAsync<ProductGetByIdQuery, ProductGetByIdResponse>(new ProductGetByIdQuery(entityId));
    }
}


//{
//  "productCategory": {
//    "name": "Smartphones",
//    "slug": "smartphones",
//    "parentCategoryId": 1
//  },
//  "productWeight": {
//    "value": 221,
//    "weightUnit": 0
//  },
//  "productDimensions": {
//    "length": 16.07,
//    "width": 7.81,
//    "height": 0.83,
//    "unit": 0
//  },
//  "productDetails": [
//    {
//      "key": "Brand",
//      "value": "Apple"
//    },
//    {
//    "key": "Model",
//      "value": "iPhone 16 Pro Max"
//    },
//    {
//    "key": "Storage",
//      "value": "256GB"
//    },
//    {
//    "key": "Color",
//      "value": "Titanium Black"
//    },
//    {
//    "key": "Screen Size",
//      "value": "6.9 inches"
//    },
//    {
//    "key": "Display",
//      "value": "Super Retina XDR OLED"
//    },
//    {
//    "key": "Resolution",
//      "value": "2796 x 1290 pixels"
//    },
//    {
//    "key": "Processor",
//      "value": "A18 Bionic chip"
//    },
//    {
//    "key": "RAM",
//      "value": "8GB"
//    },
//    {
//    "key": "Rear Camera",
//      "value": "48MP Main + 12MP Ultra Wide + 12MP Telephoto"
//    },
//    {
//    "key": "Front Camera",
//      "value": "12MP TrueDepth"
//    },
//    {
//    "key": "Battery",
//      "value": "4676 mAh"
//    },
//    {
//    "key": "Charging",
//      "value": "USB-C, MagSafe, Qi wireless"
//    },
//    {
//    "key": "Operating System",
//      "value": "iOS 18"
//    },
//    {
//    "key": "Connectivity",
//      "value": "5G, Wi-Fi 6E, Bluetooth 5.3"
//    },
//    {
//    "key": "Water Resistance",
//      "value": "IP68"
//    },
//    {
//    "key": "Face ID",
//      "value": "Yes"
//    },
//    {
//    "key": "SIM",
//      "value": "Dual eSIM + Physical Nano-SIM"
//    },
//    {
//    "key": "In the Box",
//      "value": "iPhone, USB-C Cable, Documentation"
//    }
//  ],
//  "title": "Apple iPhone 16 Pro Max",
//  "description": "The latest flagship smartphone from Apple featuring advanced camera system, A18 Bionic chip, and titanium design",
//  "price": 1199.99,
//  "sku": "IP16PM-256GB-TITANIUM"
//}