const express = require('express');
const http = require('http');
const socketIo = require('socket.io');
const app = express();
const server = http.createServer(app);
const io = socketIo(server);
const cors = require('cors');

app.use(express.json());
app.use(express.static('public'));
app.use(cors());

app.post('/notification', (req, res) => {
    const data = req.body;
    console.log('Notification received:', data);
    io.emit('notification', data);
    res.status(200).send({ status: 'ok' });
});

io.on('connection', (socket) => {
    console.log('A user connected');
})

server.listen(3000, () => console.log('Server is running on http://localhost:3000'));