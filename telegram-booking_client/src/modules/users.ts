import { Injectable } from "@nestjs/common"
import { ServerService } from "./server"
import { NestFactory } from "@nestjs/core";
import { HttpService } from "@nestjs/axios";

@Injectable()
export class Users {
  server = new ServerService(this.httpService);
  constructor(private readonly httpService: HttpService, public ctx: any) {
    this.ctx = ctx
  }
  
  start() {
    this.server.addUser(this.ctx.message.chat.id, this.ctx)
  }

  async getUsers(): Promise<void> {
    this.ctx.reply('Users list: ' + JSON.stringify(await this.server.getUsers()))
  }

  addUser(): void {
    this.ctx.reply('Write a channel_id what did you want to add')
    this.ctx.on('text', ctx => {
      let data = ctx.message.text
      this.server.addUser(data, ctx)
    })
  }

  setUser(): void {
    this.ctx.reply('Write an id and channel_id what did you want to change like `[channel_id], [id]`')
    this.ctx.on('text', ctx => {
      let text = ctx.message.text
      let data = text.split(",")
      this.server.setUser(data[0].trim(), data[1].trim(), ctx)
    })
  }

  delUser(): void {
    this.ctx.reply('Write an id what user do want to delete')
    this.ctx.on('text', ctx => {
      let data = ctx.message.text
      this.server.delUser(data, ctx)
    })
  }

  getUser(): void {
    this.ctx.reply('Write an id what user do want to get info')
    this.ctx.on('text', ctx => {
      let data = ctx.message.text
      this.server.getUser(data, ctx)
    })
  }
}
