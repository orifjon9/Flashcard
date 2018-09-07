using AutoMapper.Configuration;
using Flashcard.Models;
using Flashcard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flashcard.Web.API
{
	public class MapperProfile : MapperConfigurationExpression
	{
		public MapperProfile()
		{
			CreateMap<Card, CardViewModel>();
			CreateMap<CardViewModel, Card>();

			CreateMap<User, UserViewModel>()
				.AfterMap((user, userViewModel) => userViewModel.UserName = string.Format("{0} {1}", user.FirstName, user.LastName));
			CreateMap<UserViewModel, User>();
		}
	}
}
