import React, { useState, useEffect } from "react";
import Axios from "axios";
import Category from "../components/Category/Category";

export default function Home() {
  
  const [res1, setRes] = useState([]);
  useEffect(() => {
    async function fetchData() {
      await Axios.get(`https://hngtiendng.azurewebsites.net/api/Category`)
        .then((res) => res.data)
        .then((res) => setRes(res));
    }
    fetchData();
  }, []);
  console.log(res1);
  return (
    <div>
      <Category item={res1} />
    </div>
  );
}
