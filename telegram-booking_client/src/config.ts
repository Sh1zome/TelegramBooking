import { Injectable } from "@nestjs/common";

const ip : string = 'localhost'
const port : string = '65027'
const google_api_key : string = 'AIzaSyBzyuuBTJdMpvDH_De_PDQSrsbqf8xAXJo'

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