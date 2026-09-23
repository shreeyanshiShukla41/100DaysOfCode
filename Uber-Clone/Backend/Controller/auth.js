import jsonwebtoken from "jsonwebtoken";
import bcryptjs from "bcryptjs";
import User from "../model/User.js";

export const register = async (req, res, next) => {
  try {
    const { name, email, password, role } = req.body;

    if (!name || !email || !password || !role) {
      res.send("Fill all details");
    } else {
      const alreadyExists = await User.findOne({ email });

      if (alreadyExists) {
        res.status(400).json({ message: "User already exists" });
      } else {
        const newUser = new User({ name, email });
        bcryptjs.hash(password, 10, (err, hashedPass) => {
          newUser.password = hashedPass;
          newUser.save();
          next();
        });
        return res
          .status(201)
          .json({ message: "User successfully created", user: newUser });
      }
    }
  } catch (e) {
    console.log("Error: ", e);
  }
};

export const login = async (req, res, next) => {
  const { name, email, password, role } = req.body;

  if (!name || !email || !password || !role) {
    res.json({ message: "Fill all details" });
  } else {
    const user = await User.findOne({ email });

    if (!user) {
      res.send("User does not exist");
    } else {
      const validatedUser = bcryptjs.compare(password, user.password);

      if (!validatedUser) {
        res.send("user's email or password is incorrect");
      } else {
        const payload = {
          userId: user._id,
          email: user.email,
        };
        const JWT_SECRET_KEY =
          process.env.JWT_SECRET_KEY || "THIS_IS_A_JWT_SECRET_KEY";

        const token = jsonwebtoken.sign(payload, JWT_SECRET_KEY, {
          expiresIn: 84600 || "7d",
        });
        await User.updateOne(
          { _id: user._id },
          {
            $set: { token },
          }
        );
        res.status(200).json({ user });
      }
    }
  }
};
