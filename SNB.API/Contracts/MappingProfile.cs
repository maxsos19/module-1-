﻿using AutoMapper;
using SNB.BLL.ViewModels.Comments;
using SNB.BLL.ViewModels.Posts;
using SNB.BLL.ViewModels.Tags;
using SNB.BLL.ViewModels.Users;
using SNB.DAL.Models;

namespace SNB.API.Contracts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterViewModel, User>()
                .ForMember(x => x.Email, opt => opt.MapFrom(c => c.Email))
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.UserName));

            CreateMap<CommentCreateViewModel, Comment>();
            CreateMap<CommentEditViewModel, Comment>();
            CreateMap<PostCreateViewModel, Post>();
            CreateMap<PostEditViewModel, Post>();
            CreateMap<TagCreateViewModel, Tag>();
            CreateMap<TagEditViewModel, Tag>();
            CreateMap<UserEditViewModel, User>();
        }
    }
}
