﻿using AutoMapper;
using GigHubWebApp.DTOs;
using GigHubWebApp.Models;

namespace GigHubWebApp {
    public class MappingProfile : Profile {

        public MappingProfile() {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicationUser, UserDto>();
                cfg.CreateMap<Gig, GigDto>();
                cfg.CreateMap<Notification, NotificationDto>();
            });

            config.CreateMapper();
        }
    }
}