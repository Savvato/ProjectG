﻿namespace ProjectG.BasketService.WriteApi.DTO
{
    public class ProductUpdatedEventModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Count { get; set; } = 0;

        public float Price { get; set; }
    }
}