import React, { useState, useEffect } from 'react';
import Axios from "axios";
export default function Banner() {
  const [res1, setRes] = useState([]);
  useEffect(() => {
    async function fetchData() {
      await Axios.get(`https://localhost:44309/api/Banner`)
        .then((res) => res.data)
        .then((res) => setRes(res));
    }
    fetchData();
  }, []);
  console.log(res1);
  return (
    <ul>
      {res1.map((e, i) => {
        return <li key={i}>
            {e.productID}
        </li>;
      })}
    </ul>
  );
}
