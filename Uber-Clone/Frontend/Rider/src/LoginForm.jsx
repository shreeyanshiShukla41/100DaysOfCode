import { useState } from "react";

export default function LoginForm() {
  const [user, setUser] = useState({
    name: "",
    email: "",
    password: "",
  });

  const handleUserInfo = (e) => {
    // setUser({
    //   ...user,
    //   [e.target.name]: e.target.value, // dynamic update
    // });
    console.log(e.target.value)
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    // const res = await fetch("http://localhost:5000/api/auth/login", {
    //   method: "POST",
    //   headers: { "Content-Type": "application/json" },
    //   body: JSON.stringify(user),
    // });

    // const data = await res.json();
    // console.log("LOGIN RESPONSE:", data);

    // if (data.token) {
    //   localStorage.setItem("token", data.token); // store token
    //   alert("Login Successful!");
    //   window.location.href = "/app"; // redirect
    // } else {
    //   alert(data.message || "Login failed");
    // }
  };

  return (
    <>
      <form
        onSubmit={handleSubmit}
        style={{ display: "flex", flexDirection: "column", gap: "10px" }}
      >
        <h3>Login Form</h3>

        <input
          name="name"
          placeholder="name"
          value={user.name}
          onChange={handleUserInfo}
        />

        <input
          name="email"
          placeholder="email"
          value={user.email}
          onChange={handleUserInfo}
        />

        <input
          name="password"
          type="password"
          placeholder="password"
          value={user.password}
          onChange={handleUserInfo}
        />

        <button>Submit</button>
      </form>
    </>
  );
}
