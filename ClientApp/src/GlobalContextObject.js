import jwt_decode from "jwt-decode";
class GlobalContextObject{
    //url = "https://localhost" // testing
    url = window.location.origin // production
    hostname = window.location.hostname
    sslPort = 7050
    token;
    constructor(){
        const cookies = document.cookie.split("; ")
        for (let i = 0; i < cookies.length; i++){
            const cookie = cookies[i]
            if (cookie.startsWith("gameshub_token"))
            {
                const token = cookie.substring(15)
                this.token = jwt_decode(token)
            }
        }
    }
}

export default GlobalContextObject