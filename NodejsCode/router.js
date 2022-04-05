// 초기설정.

const express = require('express'); const app = express(); 
app.use("/", (req, res)=>{ res.sendFile('./index.html')});

const HTTPServer = app.listen(3000, ()=>{ console.log("Server is open at port:3000"); });

const wsModule = require('ws');
const webSocketServer = new wsModule.Server(
{
    server: HTTPServer
});




var clients = [];
function SendMessageAll(message)
{
    clients.forEach(ws => ws.send(message));
}

webSocketServer.on('connection', (ws, request)=>{ 
    // Request를 받으면, 접속 확인후 콘솔로그 뿌림.
    const ip = request.headers['x-forwarded-for'] || request.connection.remoteAddress;
    console.log(`새로운 클라이언트[${ip}] 접속`);  
    clients.push(ws);
    // 클라이언트 메시지 출력
    if(ws.readyState === ws.OPEN){ 
        ws.send(`클라이언트[${ip}] 접속을 환영합니다 from 서버`); 
    } 

    
    //     3) 클라이언트로부터 메시지 수신 이벤트 처리 
    ws.on('message', (msg)=>{ 
        console.log(`클라이언트[${ip}]에게 수신한 메시지 : ${msg}`);
        let jsonData = JSON.parse(msg);
        if(jsonData.type == "채팅")
        {
            SendMessageAll(jsonData.data);
        }
    });

    ws.on('error', (error)=>{ 
        console.log(`클라이언트[${ip}] 연결 에러발생 : ${error}`); 
    }) 

    ws.on('close', ()=>{ console.log(`클라이언트[${ip}] 웹소켓 연결 종료`); }) 
});
