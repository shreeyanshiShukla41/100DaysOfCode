import dotenv from "dotenv";

dotenv.config();

import express from "express";
const app = express();
import AuthRouter from "./Routes/auth.js";

app.use(express.json());
app.use(express.urlencoded({ extended: true }));
app.use("/api/auth/", AuthRouter);

const port = process.env.PORT || 4000;

console.log(port);

app.get("/", (req, res) => {
  console.log(port);
  res.send("hello, it's port number 4000");
});

app.listen(port, () => {
  console.log("port number 4000 or 8000");
});
