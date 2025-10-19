using System;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Mvc;
namespace BlogApp.ViewComponents;

public class TagsMenu: ViewComponent
{
    private IRepository<Tag> _tagRepository;
    public TagsMenu(IRepository<Tag> tagRepository)
    {
        _tagRepository = tagRepository;
    }
    
        
    public IViewComponentResult Invoke()
    {
        var tags = _tagRepository.GetList();
        return View(tags);
    }
}
