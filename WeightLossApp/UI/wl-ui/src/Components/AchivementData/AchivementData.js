import { useState, useEffect } from "react";
import React from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col, Table, InputGroup, FormControl } from "react-bootstrap";

function AchivementData() {
	// AchivementData data
	const [achivementData, setAchivementData] = useState([]);
	const [achivement, setAchivement] = useState();

	const [addShow, setAddShow] = useState(false);
	const addHandleClose = () => setAddShow(false);
	const addHandleShow = () => setAddShow(true);

	const [editShow, setEditShow] = useState(false);
	const editHandleClose = () => setEditShow(false);
	const editHandleShow = () => setEditShow(true);

	const [search, setSearch] = useState("");
	const [filterParameters, setFilterParameters] = useState({
		Name: false,
		Description: false,
		RewardExperience: false,
		ImgName: false
	});
}

export default AchivementData;
