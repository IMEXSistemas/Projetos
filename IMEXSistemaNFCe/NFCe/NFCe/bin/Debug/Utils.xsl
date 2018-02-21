<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:fo="http://www.w3.org/1999/XSL/Format"
	xmlns:n="http://www.portalfiscal.inf.br/nfe"
	xmlns:s="http://www.w3.org/2000/09/xmldsig#"
	version="2.0"
	exclude-result-prefixes="fo n s">
  <xsl:decimal-format decimal-separator="," grouping-separator="."/>
  <xsl:output method="html" />
  
  <!-- formatação de data simples "YYYY-MM-DD" para "DD/MM/YYYY" -->
  <xsl:template match="formatDate" name="formatDate">
    <xsl:param name="date"/>
    <xsl:if test="string-length($date) != 0">
      <xsl:variable name="year" select="substring-before($date, '-')"/>
      <xsl:variable name="month" select="substring-before(substring-after($date, '-'), '-')"/>
      <xsl:variable name="day" select="substring-after(substring-after($date, '-'), '-')"/>
      <xsl:value-of select="concat('', $day, '/', $month, '/', $year, '')"/>
    </xsl:if>
  </xsl:template>
  
  <!-- formatação de tempo em uma SQL Date "YYYY-MM-DDTHH:MM:SS" para "HH:MM:SS" -->
  <xsl:template name="formatTime">
    <xsl:param name="dateTime"/>
    <xsl:if test="string-length($dateTime) != 0">
      <xsl:value-of select="concat('', substring-after($dateTime, 'T'), '')"/>
    </xsl:if>
  </xsl:template>

  <!-- formatação completa de SQL Date "YYYY-MM-DDTHH:MM:SS" para "DD/MM/YYYY HH:MM:SS" -->
  <xsl:template match="formatDateTime" name="formatDateTime">
    <xsl:param name="dateTime"/>
    <xsl:param name="include_as"/>
    <xsl:if test="string-length($dateTime) != 0">
      <xsl:variable name="date" select="substring-before($dateTime, 'T')"/>
      <xsl:variable name="year" select="substring-before($date, '-')"/>
      <xsl:variable name="month" select="substring-before(substring-after($date, '-'), '-')"/>
      <xsl:variable name="day" select="substring-after(substring-after($date, '-'), '-')"/>
      <xsl:value-of select="concat('', $day, '/', $month, '/', $year, '', ' ')"/>
      <xsl:if test="$include_as !=''">
        às
      </xsl:if>
	  <xsl:value-of select="substring(substring-after($dateTime, 'T'),1,8)"/>	  
    </xsl:if>
  </xsl:template>

  <!-- formatação completa de SQL Date "YYYY-MM-DDTHH:MM:SS" para "DD/MM/YYYY HH:MM:SS" -->
  <xsl:template match="formatDateTimeFuso" name="formatDateTimeFuso">
    <xsl:param name="dateTime"/>
    <xsl:param name="include_as"/>
    <xsl:if test="string-length($dateTime) != 0">
      <xsl:variable name="date" select="substring-before($dateTime, 'T')"/>
      <xsl:variable name="year" select="substring-before($date, '-')"/>
      <xsl:variable name="month" select="substring-before(substring-after($date, '-'), '-')"/>
      <xsl:variable name="day" select="substring-after(substring-after($date, '-'), '-')"/>
      <xsl:value-of select="concat('', $day, '/', $month, '/', $year, '', ' ')"/>
      <xsl:if test="$include_as !=''">
        às
      </xsl:if>
      <xsl:variable name="horacompleta" select="concat('', substring-after($dateTime, 'T'), '')"/>
      <xsl:variable name="fuso" select="substring-after(substring-before($horacompleta, '-'), '')"/>
      <xsl:value-of select="$fuso"/>
     </xsl:if>
  </xsl:template>
  
  <!-- formatação de moeda com duas casas decimais (##,##) -->
  <xsl:template match="format2Casas" name="format2Casas">
    <xsl:param name="num"/>
    <xsl:if test="string-length($num) != 0">
      <xsl:value-of select="concat('', format-number($num,'##.##.##0,00'), '')"/>
    </xsl:if>
	<xsl:if test="string-length($num) = 0">
      <xsl:value-of select="concat('', format-number(0,'##.##.##0,00'), '')"/>
    </xsl:if>
  </xsl:template>

  <!-- formatação de numero com 3 casas decimais (##,###) -->
  <xsl:template match="format3Casas" name="format3Casas">
    <xsl:param name="num"/>
    <xsl:if test="string-length($num) != 0">
      <xsl:value-of select="concat('', format-number($num,'###.###.###.##0,000'), '')"/>
    </xsl:if>
  </xsl:template>

  <!-- formatação de numero com quatro casas decimais (##,####) -->
  <xsl:template match="format4Casas" name="format4Casas">
    <xsl:param name="num"/>
    <xsl:if test="string-length($num) != 0">
      <xsl:value-of select="concat('', format-number($num,'###.###.###.##0,0000'), '')"/>
    </xsl:if>
  </xsl:template>

  <!-- formatação de CNPJ (##.###.###-####/## -->
  <xsl:template match="formatCnpj" name="formatCnpj">
    <xsl:param name="cnpj"/>
    <xsl:if test="string-length($cnpj) != 0">
      <xsl:value-of select="concat(substring($cnpj,1,2), '.', substring($cnpj,3,3), '.', substring($cnpj,6,3), '/', substring($cnpj,9,4), '-', substring($cnpj,13,2))"/>
    </xsl:if>
  </xsl:template>

  <!-- formatação de CPF (###.###.###/##)-->
  <xsl:template match="formatCpf" name="formatCpf">
    <xsl:param name="cpf"/>
    <xsl:if test="string-length($cpf) != 0">
      <xsl:value-of select="concat(substring($cpf, 1, 3), '.', substring($cpf, 4, 3), '.', substring($cpf, 7, 3), '-', substring($cpf, 10, 2))"/>
    </xsl:if>
  </xsl:template>

  <!-- formatação de CEP (#####-###)-->
  <xsl:template match="formatCep" name="formatCep">
    <xsl:param name="cep"/>
    <xsl:if test="string-length($cep) != 0">
      <xsl:value-of select="concat(substring($cep,1,5), '-', substring($cep,6,3))"/>
    </xsl:if>
  </xsl:template>
    
  <!-- formatação de números de telefone "####-####", "(##)####-####" e "(###)####-####" -->
  <xsl:template match="formatFone" name="formatFone">
    <xsl:param name="fone"/>
    <xsl:if test="string-length($fone) != 0">
      <xsl:choose>
        <xsl:when test="string-length($fone) = 8">
          <xsl:value-of select="concat(substring($fone,1,4), '-', substring($fone,5,4))"/>
        </xsl:when>
        <xsl:when test="string-length($fone) = 10">
          <xsl:value-of select="concat('(', substring($fone,1,2),')', substring($fone,3,4), '-', substring($fone,7,4))"/>
        </xsl:when>
        <xsl:when test="string-length($fone) = 11">
          <xsl:value-of select="concat('(', substring($fone,1,3),')', substring($fone,4,4), '-', substring($fone,8,4))"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="$fone"/>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:if>
  </xsl:template>

  <!-- formatação da chave de acesso da NFe -->
  <xsl:template match="formatNfe" name="formatNfe">
    <xsl:param name="nfe"/>
    <xsl:if test="string-length($nfe) != 0">
      <xsl:value-of select="concat(
									substring($nfe,1,4), ' ',
									substring($nfe,5,4), ' ',
									substring($nfe,9,4), ' ',
									substring($nfe,13,4), ' ',
									substring($nfe,17,4), ' ',
									substring($nfe,21,4), ' ',
									substring($nfe,25,4), ' ',
									substring($nfe,29,4), ' ',
									substring($nfe,33,4), ' ',
									substring($nfe,37,4), ' ',
									substring($nfe,41,4))"/>
    </xsl:if>
  </xsl:template>
</xsl:stylesheet>