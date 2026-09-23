import express from "express";
const router = express.Router();
import { login, register } from "../Controller/auth.js";
import { verifyToken } from "../MiddleWare/auth.js";

router.post("/login", login);
router.post("/register", register);
router.get("/verifyUser", verifyToken, (req, res) => {
  console.log("verify user");
});
export default router;
