import { Injectable } from "@nestjs/common";

const ip : string = 'localhost'
const port : string = '52437'

@Injectable()
export class Config {
    getIp() {
        return ip
    }
    getPort() {
        return port
    }
    getAddress() {
        return 'https://' + this.getIp() + ':' + this.getPort()
    }
}