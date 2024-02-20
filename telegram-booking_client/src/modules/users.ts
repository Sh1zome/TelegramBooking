import { Injectable } from "@nestjs/common"
import { ServerService } from "./server"
import { NestFactory } from "@nestjs/core";
import { HttpService } from "@nestjs/axios";
import { Markup } from 'telegraf';

@Injectable()
export class Users {
  server = new ServerService(this.httpService);
  bot: any;
  constructor(private readonly httpService: HttpService, bot: any) {
    this.bot = bot
  }
  
  start(ctx) {
    this.server.addUser(ctx.message.chat.id, ctx)
  }

  async getUsers(ctx, buttonList): Promise<void> {
    ctx.reply('Список всех пользователей: ' + JSON.stringify(await this.server.getUsers()))
  }

  async addUser(ctx, buttonList): Promise<void> {
    let data = ctx.message.text
    let res = JSON.stringify(await this.server.addUser(data, ctx))
    ctx.reply('Пользователь с ID чата ' + data + ' добавлен', buttonList)
  }

  setUser(ctx, buttonList): void {
    let text = ctx.message.text
    let data = text.split(",")
    this.server.setUser(data[1].trim(), data[0].trim(), ctx)
    ctx.reply('У пользователя с ID ' + data[0] + ' был изменен Channel ID на ' + data[1], buttonList)
  }

  delUser(ctx, buttonList): void {
    let data = ctx.message.text
    this.server.delUser(data, ctx)
    ctx.reply('Пользователь с ID ' + data + ' удален', buttonList)
  }

  async getUser(ctx, buttonList) {
    let data = ctx.message.text
    let res = JSON.stringify(await this.server.getUser(data, ctx))
    if (await res == 'null') {
      ctx.reply('Пользователь с ID ' + data + ' не существует', buttonList)
      return
    }
    ctx.reply('Пользователь ' + data + ': ' + await res, buttonList)
  }
}
