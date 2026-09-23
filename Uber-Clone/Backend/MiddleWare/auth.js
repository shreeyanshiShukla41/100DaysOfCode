import jsonwebtoken from "jsonwebtoken";

export const verifyToken = (req, res, next) => {
  try {
    console.log(req.headers.authorization);
    next();
  } catch (e) {
    console.log(e);
  }
};
