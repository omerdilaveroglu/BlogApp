using System;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Mvc;


namespace BlogApp.ViewComponents;

public class TagsMenu : ViewComponent
{
    private IRepository<Tag> _tagRepository;
    public TagsMenu(IRepository<Tag> tagRepository)
    {
        _tagRepository = tagRepository;
    }


    public async Task<IViewComponentResult> InvokeAsync()
    {
        var tags = await _tagRepository.GetListAsync();
        return View(tags);
    }
}
