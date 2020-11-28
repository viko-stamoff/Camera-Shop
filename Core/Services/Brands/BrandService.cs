using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Camera_Shop.Database;
using Data.Models.Classes;

namespace Camera_Shop.Services.Brands
{
	public class BrandService
	{
		private readonly EntityRepository<Brand> _repository;
		
		public BrandService(CameraContext context)
		{
			this._repository = new EntityRepository<Brand>(context);
		}

		//Create
		public async Task CreateBrandAsync(Brand brand)
		{
			//Null check
			if(brand == null)
				throw new ArgumentNullException("Brand cannot be null!");
			
			//Check for brand in database
			if(DoesBrandExist(brand))
				throw new ArgumentException($"Brand {brand.Name} exists!");
			
			await this._repository.AddAsync(brand);
		}
		
		//Read
		public IEnumerable<Brand> GetAllBrands()
		{
			return this._repository.QueryAll();
		}
		
		public Brand GetBrand(int id)
		{
			return this._repository.QueryAll()
				.FirstOrDefault(x => x.Id == id);
		}
		
		//Update
		public async Task UpdateBrandAsync(int id, Brand brand)
		{
			//Null check for given brand
			if(brand == null)
				throw new ArgumentNullException("Brand cannot be null!");
			
			//Check for brand in database
			if(DoesBrandExist(brand))
				throw new ArgumentException($"Brand {brand.Name} exists!");

			var brandToModify = this._repository.QueryAll()
				.FirstOrDefault(x => x.Id == id);

			//Null check for brand that should be modified
			if(brandToModify == null)
				throw new ArgumentException($"Brand {brand.Name} does not exist!");
			
			await this._repository.EditAsync(id, brand);
		}
		
		//Delete
		public async Task DeleteBrandAsync(int id)
		{
			var brandToDelete = this._repository.QueryAll()
				.FirstOrDefault(x => x.Id == id);

			//Null check for brand that should be deleted
			if(brandToDelete == null)
			{
				throw new ArgumentException("Brand doesn't exist");
			}
			
			await this._repository.DeleteAsync(brandToDelete);
		}
		
		//Validations
		private bool DoesBrandExist(Brand brand)
		{
			return this._repository.QueryAll()
				.Any(x => x.Name == brand.Name);
		}
	}
}