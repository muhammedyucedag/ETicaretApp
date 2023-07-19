﻿using ETicaretAPI.Application.Abstractions.Services.Configurations;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.DTOs.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace ETicaretAPI.Infrastructure.Services.Configurations
{
    public class ApplicationService : IApplicationService
    {
        public List<MenuDto> GetAuthorizeDefinitionEndPoints(Type type)
        {
            //Öncelikle çalışan assembly neyse onu çağırıyoruz
            //Tüm metotları elde ediyoruz
            Assembly assembly = Assembly.GetAssembly(type);
            var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));

            List<MenuDto> menus = new();
            if (controllers != null)
            {
                foreach (var controller in controllers)
                {
                    //AuthorizeDefinitionAttribute işaretli tüm metotları getirecektir.
                    var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                    if (actions != null)
                    {
                        foreach (var action in actions)
                        {
                            var attributes = action.GetCustomAttributes(true);
                            if (attributes != null)
                            {
                                MenuDto menu = null;

                                var authorizeDefinitionAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                                if (!menus.Any(m => m.Name == authorizeDefinitionAttribute.Menu))
                                {
                                    menu = new() { Name = authorizeDefinitionAttribute.Menu };
                                    menus.Add(menu);
                                }
                                else
                                    menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitionAttribute.Menu);

                                ActionDto actionDto = new()
                                {
                                    ActionType = authorizeDefinitionAttribute.ActionType,
                                    Definition = authorizeDefinitionAttribute.Definition,
                                };

                                //HttpMethodAttribute bunan kalıtım almış deperi getiriyor
                                var httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                                if (httpAttribute != null)
                                    actionDto.HttpType = httpAttribute.HttpMethods.First();
                                else
                                    actionDto.HttpType = HttpMethods.Get; // Http değerini get olarak işaretliyoruz. (http gelmezse)
                                menu.Actions.Add(actionDto);
                                
                            }
                        }
                    }
                }
            }
            
            return menus;
        }
    }
}
