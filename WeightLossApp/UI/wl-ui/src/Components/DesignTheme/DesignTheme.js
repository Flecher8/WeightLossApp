import { useState, useEffect } from "react";
import React from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col, Table, InputGroup, FormControl } from "react-bootstrap";

function DesignThemeData() {
    // Design theme data
    const [designThemes, setDesignThemes] = useState([]);
    const [designTheme, setDesignTheme] = useState();

    const [addShow, setAddShow] = useState(false);
	const addHandleClose = () => setAddShow(false);
	const addHandleShow = () => setAddShow(true);

    const [editShow, setEditShow] = useState(false);
	const editHandleClose = () => setEditShow(false);
	const editHandleShow = () => setEditShow(true);

    const [search, setSearch] = useState("");
	const [filterParameters, setFilterParameters] = useState({
		BaseColor: false,
		SecondaryColor: false,
		AccentColor: false
	});

    // Get exercises data
	function getDesignThemes() {
		fetch(constants.API_URL + "DesignThemeData")
			.then(res => res.json())
			.then(data => setDesignThemes(data));
	}

	// onLoad function
	useEffect(() => {
		getDesignThemes();
	}, []);
}

export default DesignThemeData;